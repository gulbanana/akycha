dotnet publish -c Release
Compress-Archive -Path .\bin\Release\net8.0\publish\* -DestinationPath .\bin\Release\net8.0\publish.zip -Force
#az login --tenant a9c9b3ac-08e2-42f3-b550-80fbbd5cd18c
az webapp deploy --type zip --src-path .\bin\Release\net8.0\publish.zip -g personal -n akycha