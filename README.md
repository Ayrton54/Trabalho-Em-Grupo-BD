# Trabalho-Em-Grupo-BD
Projeto desenvolvido como parte do trabalho da faculdade.

## Desafio Proposto:
Antes de começar, este projeto tem como desafio:

![Caso de uso](https://github.com/Ayrton54/Trabalho-Em-Grupo-BD/assets/106751266/8ef9147d-46f8-4011-9f2e-d270832334cc)

1. **Gerar o Modelo Lógico.**
2. **Criar o Banco de Dados.**
3. **Povoar as Tabelas para a Verificação das Consultas.** (Pelo menos 10 linhas em cada tabela)
4. **Desenvolver uma Aplicação em Qualquer Linguagem de Programação que se Conecte ao Banco de Dados.**
5. **Apresentar os Códigos para as Consultas Solicitadas.**

### Consultas:
1. Listar os empréstimos realizados em uma determinada data.
2. Listar todos os e-mails de um determinado autor.
3. Listar os clientes de um determinado bairro.
4. Listar exemplares adquiridos em determinada data.
5. Listar código, nome e e-mails dos autores.
6. Listar código, nome e e-mails dos clientes.
7. Listar nome do autor de um determinado livro.
8. Apresentar a média dos preços dos livros.
9. Listar nome da editora de um determinado livro.
10. Listar o nome e data de aquisição de um determinado livro.

A apresentação deve incluir o funcionamento do sistema desenvolvido, cobrindo cadastro (inserção, alteração e exclusão de registros) 
e a listagem das consultas na tela. Também podem ser solicitadas alterações no banco durante a apresentação.

## Instalação

É necesario ter instalado na sua maquina: 
- SqlServer
- C#
- Uma IDE de sua preferência.

## Passos para conseguir rodar o projeto

### Criando a API: 
dotnet new webapi

### iniciar api: 
dotnet watch run

### ferramentas para trabalhar:
só precisa instalar uma vez por maquina.
dotnet tool install --glibal dotnet-ef

## sempre precisa installar
dotnet add package Microsoft.EntityFrameWorkCore.Design --version 7.0.0

dotnet add package Microsoft.EntityFrameWorkCore.SqlServer --version 7.0.0

### Criando as migrations
dotnet-ef migrations add CriacaoTabela

### Criando no banco  de dados
dotnet-ef database update 

## Testes com Swagger
Este projeto utiliza o Swagger para facilitar os testes. Após iniciar a API



