$shouldCreatecertificate = Read-Host -Propmpt  "Do you want to create SSL certificate for image? (Choose yes if not done previously) y/n"
        if (($shouldCreatecertificate -eq 'y') -or ($shouldCreatecertificate -eq '')) {
            dotnet dev-certs https --clean
            dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\Diploma.pfx -p crypticpassword
            dotnet dev-certs https --trust
        }

docker-compose build
docker-compose up