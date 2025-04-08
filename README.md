# 🛒 E-Commerce API

Este guia explica como configurar, executar e administrar a API de E-Commerce desenvolvida em **.NET 6**, com **MySQL** e **autenticação JWT**.

---

## 📋 Pré-requisitos

Certifique-se de ter os seguintes itens instalados:

- ✅ [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- ✅ [MySQL Server 8.0+](https://dev.mysql.com/downloads/mysql/)
- ✅ [Git](https://git-scm.com/)
- ✅ Editor de código como **Visual Studio** (utilizado no desenvolvimento) ou **VS Code**

---

## 🚀 Configuração Inicial

### 1️⃣ Clone o repositório

```bash
git clone https://github.com/seu-usuario/e-commerce-api.git
cd e-commerce-api
```

### 2️⃣ Configure o banco de dados MySQL

- Crie o banco de dados:

```sql
CREATE DATABASE EcommerceAPI;
```

- Edite o arquivo `appsettings.json` com suas credenciais:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=ecommerce_db;user=root;password=sua_senha"
}
```

### 3️⃣ Instale as dependências

```bash
dotnet restore
```

---

## ⚙️ Migrações do Banco de Dados

### 🏗️ Criar migração inicial

```bash
dotnet ef migrations add InitialCreate --project E-Commerce-API.csproj
```

### 📦 Aplicar migrações

```bash
dotnet ef database update --project E-Commerce-API.csproj
```

> 💡 **Visual Studio**: você pode usar o **Package Manager Console**:
```powershell
Add-Migration InitialCreate
Update-Database
```

---

## 🔐 Configuração do JWT

- Gere uma chave secreta (mínimo 32 caracteres) e edite o `appsettings.json`:

```json
"Jwt": {
  "Key": "SUA_CHAVE_SECRETA_AQUI_1234567890abcdefghijklmnopqrstuv",
  "Issuer": "E-Commerce-API",
  "Audience": "E-Commerce-Client",
}
```

---

## 🏃 Executando a API

### 🔧 Modo de Desenvolvimento

```bash
dotnet run
```

- Acesse:
  - API: `https://localhost:5001`
  - Swagger UI: `https://localhost:5001/swagger`

### 📦 Modo de Produção

```bash
dotnet publish -c Release -o ./publish
cd publish
dotnet E-Commerce-API.dll
```

---

## 🔌 Principais Endpoints

| Método | Rota                                      | Descrição                          |
|--------|-------------------------------------------|------------------------------------|
| POST   | `/api/auth/registrar`                    | Registrar novo usuário             |
| POST   | `/api/auth/login`                        | Fazer login                        |
| GET    | `/api/produtos`                          | Listar todos os produtos           |
| POST   | `/api/produtos`                          | Criar produto (requer autenticação)|
| GET    | `/api/carrinho`                          | Ver carrinho (requer autenticação) |
| POST   | `/api/carrinho/adicionar/{produtoId}`    | Adicionar item ao carrinho         |

---

## ⚡ Variáveis de Ambiente (Produção)

No ambiente de produção, configure:

```env
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=server=localhost;database=ecommerce_db;user=root;password=sua_senha
Jwt__Key=SUA_CHAVE_SECRETA_AQUI_1234567890abcdefghijklmnopqrstuv
```

---

## 📦 Dependências do Projeto

Principais pacotes NuGet utilizados:

- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore`
- `Pomelo.EntityFrameworkCore.MySql`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Swashbuckle.AspNetCore` (Swagger)

---

## 🔧 Solução de Problemas

### ❌ Erros de Migração

- Verifique se o MySQL está rodando
- Confirme a `connection string` em `appsettings.json`
- Tente recriar as migrações:

```bash
rm -r Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### ❌ Problemas com JWT

- A chave JWT deve ter **mínimo 32 caracteres**
- Certifique-se que `UseAuthentication()` vem antes de `UseAuthorization()`

### ❌ Erros no Swagger

- Limpe e reconstrua o projeto:

```bash
dotnet clean
dotnet build
```

- Verifique se os controllers estão corretamente anotados com `[ApiController]` e `[Route]`

---

## 📜 Licença

Distribuído sob a licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais informações.

---

## 🎉 Pronto!

Sua API está configurada e pronta para uso! 🚀
