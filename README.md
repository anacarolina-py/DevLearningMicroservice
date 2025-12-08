# 📘 DevLearning -- Student & StudentCourse Microservice

Microserviço responsável pelo gerenciamento de **estudantes** e
**matrículas de cursos** dentro da plataforma DevLearning.\
Desenvolvido em **C# .NET**, utilizando **Dapper** como ORM e **SQL
Server** como banco de dados.

## 🚀 Tecnologias Utilizadas

-   **.NET 9**
-   **C#**
-   **Dapper**
-   **SQL Server**
-   **REST API**
-   **Architecture: Microservices**

## 📂 Estrutura do Microservice

Este microserviço contém duas áreas principais:

### 1. Student

Responsável pelo CRUD de estudantes.

### 2. StudentCourse

Gerencia as matrículas, o progresso e favoritos dos estudantes dentro
dos cursos.

# ⚠️ Pontos de Vulnerabilidade Identificados

## 1. Falta de verificação para documento e e-mail

Não existe validação para saber se documento ou e‑mail já estão
cadastrados.

## 2. Falta de tratamento de erros na camada de serviço

Não há uso de try/catch, causando falhas não tratadas.

## 3. Service sem interface

Sem interfaces como IStudentService ou IStudentCourseService, quebrando
práticas de abstração.

## 4. StudentCourse sem validação de existência de entidades

Não valida se o estudante e o curso realmente existem.

## 5. Falta de verificação de matrícula duplicada

Permite cadastrar a mesma matrícula mais de uma vez.

# 🛠 Estrutura Recomendada para Correções

-   Criar interfaces
-   Adicionar validações de e-mail/documento
-   Garantir validação de existência no StudentCourse
-   Envolver lógica de serviço em try/catch
-   Verificar se a matrícula já existe
