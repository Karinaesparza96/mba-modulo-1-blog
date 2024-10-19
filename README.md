# **✨ PostShare - Aplicação de Blog Simples com MVC e API RESTful**

## **1. Apresentação**
Bem-vindo ao repositório do projeto **PostShare**! Este projeto é uma entrega do MBA DevXpert Full Stack .NET e faz parte do módulo **Introdução ao Desenvolvimento ASP.NET Core**.

### **🎯 Objetivo**
Desenvolver uma aplicação de blog que permite aos usuários criar, editar, visualizar e excluir posts e comentários, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful.

### **👤 Autor(es)**
- **Karina Esparza**

## **2. Proposta do Projeto**
O projeto inclui:
- **🖥 Aplicação MVC:** Interface web para interação com o blog.
- **🌐 API RESTful:** Exposição dos recursos do blog para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **🔒 Autenticação e Autorização:** Controle de acesso, diferenciando administradores e usuários comuns.
- **💾 Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**
- **🛠 Linguagem de Programação:** C#
- **📚 Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **💾 Banco de Dados:** SQLite
- **🔐 Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **🎨 Front-end:**
  - Razor Pages/Views
  - HTML/CSS e Bootstrap para estilização
  - jQuery para requisições AJAX
- **📄 Documentação da API:** Swagger

## **4. Estrutura do Projeto**
A estrutura do projeto é organizada da seguinte forma:

```
src/
  ├── BlogApp/          # Projeto MVC
  ├── BlogApi/          # API RESTful
  ├── BlogCore/         # Modelos de Dados, Interfaces e Configuração do EF Core
README.md               # Arquivo de Documentação do Projeto
FEEDBACK.md             # Arquivo para Consolidação dos Feedbacks
.gitignore              # Arquivo de Ignoração do Git
```

## **5. Funcionalidades Implementadas**
- **📝 CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir posts e comentários.
- **🔑 Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **🌍 API RESTful:** Exposição de endpoints para operações CRUD via API.
- **📑 Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**
- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**
1. **Clone o Repositório:**
   ```bash
   git clone https://github.com/Karinaesparza96/mba-modulo-1-blog.git
   cd mba-modulo-1-blog
   ```

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

3. **Executar a Aplicação MVC:**
   ```bash
   cd src/BlogApp/
   dotnet run
   ```
   - Acesse a aplicação em: [http://localhost:5009](http://localhost:5009)

4. **Executar a API:**
   ```bash
   cd src/BlogApi/
   dotnet run
   ```
   - Acesse a documentação da API em: [http://localhost:5001/swagger](http://localhost:5001/swagger)

## **7. Instruções de Configuração**
- **🔑 JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **⚙️ Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido à configuração do Seed de dados.

## **8. Documentação da API**
A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em: [http://localhost:5001/swagger](http://localhost:5001/swagger)

## **9. Avaliação**
- Este projeto é parte de um curso acadêmico e não aceita contribuições externas.
- Para feedbacks ou dúvidas, utilize o recurso de Issues.
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
