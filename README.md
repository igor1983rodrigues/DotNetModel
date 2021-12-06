docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=applications@4pl1c4710n" \
   -p 1433:1433 --name sql1 -h sql1 \
   -d mcr.microsoft.com/mssql/server:2019-latest


/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "applications@4pl1c4710n"