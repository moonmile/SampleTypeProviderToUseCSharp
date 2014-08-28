namespace SampleTypeProv
open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open System.Reflection

[<TypeProvider>]
type CSharpTypeProv(config:TypeProviderConfig) as this = 
    inherit TypeProviderForNamespaces()
    let namespaceName = "CSharpTypeProvider" 
    let thisAssembly = Assembly.GetExecutingAssembly()


    /// 型生成を残す場合
    /// [<Litelal>]
    /// let xaml = "<ContentPage>...</ContentPage>"
    /// type MainPage = Moonmile.XamarinFormsTypeProvider.XAML< xaml >
    // 型の定義
    let t = ProvidedTypeDefinition(thisAssembly, namespaceName, "STR", Some(typeof<obj>), IsErased = false )
    do t.DefineStaticParameters(
        [ProvidedStaticParameter("str", typeof<string>)],
        fun typeName parameterValues ->
            let str = string parameterValues.[0]
            let outerType = 
                ProvidedTypeDefinition (thisAssembly, namespaceName, 
                    typeName, Some(typeof<obj>), IsErased = false )
            // 名前を付けてアセンブリを残す
            // let tempAssembly = ProvidedAssembly(System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), ".dll"))
            // フルパスで指定する
            // 型を残して C# から直接参照できるようにする
            let tempAssembly = ProvidedAssembly("d:\\temp\\SampleType."+ str+".dll")
            tempAssembly.AddTypes <| [ outerType ]
            // コンパイル時に試しに読み込んでみる
            // コンストラクタの生成
            let ctor = ProvidedConstructor([], 
                            // 仮に引数を返しておく
                            InvokeCode = fun args -> <@@ str @@>  )  
            do outerType.AddMember( ctor )

            // 名前を返すプロパティ
            let prop = 
                ProvidedProperty( "Name", typeof<string>, 
                    GetterCode = fun args -> <@@ "sample typepfovider for c# :" + str @@> )
            do outerType.AddMember( prop )

            // バージョンを返すプロパティ
            let prop = 
                ProvidedProperty( "Version", typeof<float>, 
                    GetterCode = fun args -> <@@ 0.9 @@> )
            do outerType.AddMember( prop )


            outerType
    )
    // 名前空間に型を追加
    do this.AddNamespace( namespaceName, [t] )

[<assembly:TypeProviderAssembly>] 
do()