# Projeto Final - Escola

## Tecnologias utilizadas:
* API criada em C# com Microsoft.EntityFrameworkCore
* Banco de dados Azure SQL Server
* Deploy da API no Azure

### Como projeto final para o curso Coding Girls ministrado pela Blue EdTech, foi feita uma API simulando um sistema de escola com duas CRUDs, uma para os alunos e outra para as turmas, que deveria ser implantada no Azure. Os requisitos do projeto são:
* Um aluno não pode ser incluído sem uma turma.
* Uma turma só pode ser excluída se não tiverem alunos cadastrados nela.
* Um aluno pode ser movido de turma.
* A consulta por turmas e alunos deve obedecer uma regra que é: só retornar alunos cuja condição é ativa(o)