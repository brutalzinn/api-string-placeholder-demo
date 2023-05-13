##Baseado em https://github.com/AlexSugak/dotnet-core-tdd/blob/master/Makefile

build:
	dotnet build ApiPlaceHolderDemo.csproj

package:
	dotnet add ApiPlaceHolderDemo.csproj package $(ARGS)

rebuild-docker: 
	docker-compose down
	docker-compose build --no-cache
	docker-compose up -d

restart-docker: 
	docker-compose down
	docker-compose up -d