
# Projeto Promart

Projeto para gerenciamento de alunos, funcionários, voluntários e oficinas do Centro Comunitário Batista Sete de Setembro.


## Implementações

- Cadastro e atualização de alunos e oficinas;
- Uso de múltiplos filtros para relatórios;
- Modo de abas ao invés de janelas;
- Impressão e exportação de relatórios e outras páginas;


## Tecnologias

Microsoft SQL Server 2019: para banco de dados relacional;
EntityFramework: Como ORM para acesso ao banco.
.NET 6: e a linguagem C# como ambiente de desenvolvimento.
WPF: como GUI para janelas.


## Installation

Para testes é necessário o SQL Server 2019 e a edição da linha "connectionString" no arquivo config.json para um Data Source correto.

```bash
{
  "connectionString": "Data Source=./;Initial Catalog=PromartDesktop;Integrated Security=True;Trust Server Certificate=True"
}
```
    