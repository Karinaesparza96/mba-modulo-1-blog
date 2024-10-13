# Feedback do Instrutor

#### 11/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Boa separação de responsabilidades.
- Arquitetura compatível com o nível de complexidade do projeto.
- Uso adequado de ferramentas como AutoMapper
- Controle eficiente de usuários com autorização e roles.
- Demonstrou sólido conhecimento em Identity e JWT.
- Mostrou entendimento do ecossistema de desenvolvimento em .NET
- Boa sacada usando o IsOwnerOrAdmin
- Bom uso de extension methods para configurações
- Acertou na mão ao definir as responsabilidades em BlogCore
- Bom uso de PartialViews
- Usou bem o recurso de construtores na declaração da classe
- Documentou bem o repositório

## Pontos Negativos:

- Nada que mereça ser apontado até o momento.

## Sugestões:

- Como opção de melhoria crie uma estrutura mais elegante para a validação e retorno de mensagens de erro da classe "Messages".

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
