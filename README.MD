#Stock Wallet

Projeto simples de gestao de acoes, apenas cadastro de carteiras e comppra e venda.
Separado entre api rest (StockWallet) e portal razor (StockWallet.Portal), o Portal 
apenas acessa a api via http;
A api precisa da connection string, ou no arquivo appsettings.json "ConnectionStrings:Default"
ou em uma variavel de ambiente "ConnectionStrings__Default".

O banco de dados pode ser criado por migration ou pelo backup (mariadb) na raiz do projeto.
