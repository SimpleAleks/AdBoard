@echo off

set /p input=Enter tag version: 
echo %input%
@echo on

docker build -f "src/Host/AdBoard.Host.Api/Dockerfile" . --push -t prostoaleks/adboard-api:%input%
docker build -f "src/Host/AdBoard.Host.Migrator/Dockerfile" . --push -t prostoaleks/adboard-migrator:%input%