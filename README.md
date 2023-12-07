# Projecto Base Asp Net Core Com Identity, Gestão de Utilizadores e Roles
Criando um projecto com autenticação e autorização

A ideia inicial é de criar um projecto completo que faça o login, registo de utilizadores utilizando o Identity do asp net core
E para complementar acrescentamos a parte de gestão de utilizadores, gestão de roles e atribuição de roles aos utilizadores.

## Créditos
Esse projecto foi construido baseando-se nos seguintes tutoriais

### Microsoft
[Introduction to Identity on Asp.Net Core - Microsoft]([docs/CONTRIBUTING.md](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=netcore-cli)https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=netcore-cli)

[Identity model customization in ASP.NET Core - Microsoft](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-7.0)

### Yogi Hosting

[How to work with Roles in ASP.NET Core Identity](https://www.yogihosting.com/aspnet-core-identity-roles/)


[How to Create, Read, Update & Delete Users in ASP.NET Core Identity](https://www.yogihosting.com/aspnet-core-identity-create-read-update-delete-users/)

> [!NOTE]
> O projecto ainda está em construção. Qualquer outra referência, que vir a ser adoptada para enriquecer o projecto, será citada aqui nos créditos.

### Instalação

Estaremos criando uma aplicação `ASP.NET Core MVC` com uma autenticação `Individual`, e `LocalDB`

```
dotnet new mvc --auth Individual -uld -o WebApp1
```

Nesse momento o projecto será criado, e com ele as rotas 

* /Identity/Acount/Login
* /Identity/Account/Logout
* /Identity/Account/Manage

Mas no momento os códigos das páginas razor não se encontram disponíveis, então termos que habilitar eles

```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet aspnet-codegenerator identity -dc WebApp1.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout;Account.RegisterConfirmation"
```

Se navegarmos em `Area->Account` veremos as páginas Register, Login, Logout e RegisterConfirmation


Mas se for da tua preferencia e decidires criar as páginas personalizadas podes pular os passos anteriores e fazer o seguinte:

Vai em `Controllers` botão direito, adiciona nova controller (Se estiver a utilizar o VSCode como editor, recomendamos instalar a extensão [C# Devkit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit))

Vamos chamar ele de `ContaController`, nele irás criar 3 actions (funcções)

* Login
* Registo
* Logout



