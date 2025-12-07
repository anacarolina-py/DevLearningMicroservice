📚 DevLearning – Plataforma de Cursos (Arquitetura de Microserviços)

Este repositório apresenta a versão refatorada da DevLearning, originalmente uma aplicação monolítica, agora distribuída em múltiplos microserviços independentes, cada um responsável por um domínio específico do negócio.

A arquitetura foi desenvolvida com o objetivo de melhorar desacoplamento, escalabilidade, facilidade de manutenção, além de permitir maior flexibilidade tecnológica entre os serviços.

🏗️ Arquitetura dos Microserviços

A aplicação foi dividida nos seguintes serviços:

1. Students API (SQL)

Responsável por:

Cadastro de estudantes

Gestão de matrículas

StudentItems (cursos que o aluno possui / está consumindo)

2. Courses & Category API (SQL)

Responsável por:

Cadastro de cursos

CRUD de Categorias

Associação de cursos categorias

Consumo da Authors API para obter dados do autor

Endpoints para consulta por carreira

3. Authors API (MongoDB)

Responsável por:

Cadastro de autores

Consultado diretamente pela Courses API

5. Career API (SQL)

Responsável por:

CRUD de carreiras (Career)

CareerItems

Consumo da Courses API para montar trilhas de aprendizado

🔗 Comunicação entre os serviços

A comunicação é feita via REST, usando URLs configuradas em cada serviço.

Exemplos:

O serviço Courses chama a Authors API para retornar o autor:

GET /authors/{id}


O serviço Career consome o serviço Courses:

GET /courses/{courseId}


O serviço Students consome o serviço Courses:

GET /courses/{id}

🗃️ Banco de Dados

Cada serviço possui seu próprio banco:

Serviço	Banco
Students API	SQL Server
Courses & Category API	SQL Server
Career API	SQL Server
Authors API	MongoDB

🚀 Como Rodar a Aplicação
✔️ Pré-requisitos

Docker e Docker Compose

.NET 9+ (se for rodar localmente sem Docker)

▶️ Subindo tudo com Docker

No diretório principal:

docker-compose up -d --build


Os serviços subirão em portas diferentes, por exemplo:

Serviço	Porta
Students API	http://localhost:5001

Courses API	http://localhost:5002

Category API	http://localhost:5003

Career API	http://localhost:5004

Authors API	http://localhost:5005

(Altere conforme seu compose.)

📘 Documentação de Uso Após a Refatoração

A seguir estão exemplos reais de uso de cada microserviço.

1️⃣ Students API
➤ POST /api/student

Cria um estudante.

📤 Exemplo de requisição:

{
  "Name": "Bruno",
  "Email": "bruno@gmail.com",
  "Password": "Bruno123",
  "Bio": "Backend Developer",
  "Image": "/9j/4AAQSkZJRgABAQAAAQABAAD..." 
}

➤ GET /api/student

Retorna todos os estudantes.

➤ GET /api/student/{id}

Busca um estudante por ID.

➤ GET /api/student/document/{document}

Busca por documento.

➤ GET /api/student/email/{email}

Busca por email.

➤ PUT /api/student/{id}

Edita um estudante.

➤ PUT /api/student/{id}/courses/{courseId}

Atualiza progresso do aluno ou favorito.

📤 Exemplo:

{
  "Favorite": 2
}


📌 Exemplo de rota real usada no Postman:

https://localhost:7169/api/students/0bc3d383-b629-4fd8-bebc-d22e699c0e1b/courses/B2EE394B-6DE5-4AD2-95E7-83959918549A

2️⃣ Course API
➤ GET /api/Course

Retorna todos os cursos.

➤ GET /api/Course/get-by-title

Busca um curso pelo título.

➤ POST /api/Course

Cria um curso.

📤 Exemplo real do Postman:

{
  "tag": "backend",
  "title": "Curso de POO Básiquinho",
  "summary": "Introdução à Programação Orientada a Objetos",
  "url": "curso-poo-basica",
  "level": 3,
  "durationInMinutes": 120,
  "authorId": "ADABAAEC-199C-4533-8C53-E1B19F015311",
  "categoryId": "35AC4CB9-E3A2-4A28-8D81-E75876B918F7",
  "tags": "fundamentos de C#"
}

➤ PUT /api/Course/title

Atualiza dados de um curso (payload livre).

➤ PUT /api/Course/Active/{title}

Ativa/desativa um curso.

📤 Exemplo:

{
  "Active": false
}


📌 Exemplo real:

https://localhost:7169/api/Course/Active/Curso de POO Intermediario

3️⃣ Category API
➤ POST /api/Category

Cria uma categoria.

📤 Exemplo:

{
  "title": "Nova Categoria",
  "summary": "Descrição da categoria",
  "order": 1,
  "description": "Texto detalhado da categoria"
}

➤ GET /api/Category

Lista todas as categorias.

➤ GET /api/Category/{id}

Busca categoria por ID.

Exemplo real:

/api/Category/CEAC3794-1C8C-48AA-B7F8-18BDC811EF85

➤ PUT /api/Category/{id}

Atualiza categoria.

📤 Exemplo:

{
  "order": 3,
  "featured": true
}

➤ DELETE /api/Category/{id}

Exclui categoria.

➤ GET /api/Category/{id}/courses

Lista cursos de uma categoria.

Exemplo real:

/api/Category/d30a351d-f0e7-42c6-a6af-d4ed0fcc20c2/courses

4️⃣ Author API
➤GET /authors

Retorna todos os autores cadastrados.

➤ GET /api/author/{id}

Busca um author pelo id.

➤ POST /api/author

Cria um autor.

📤 Exemplo:

{
  "name": "João da Silva",
  "title": "Instrutor de C#",
  "image": "https://www.devlearning.com.br/images/authors/joao-silva.jpg",
  "bio": "João é instrutor de C# com 10 anos de experiência.",
  "email": "joao.silva@devlearning.com.br",
  "type": 1
}

5️⃣ Career API

➤GET /careers

Lista todas as carreiras.

➤GET /careers/{id}

Retorna:

informações da carreira


➤ POST /api/Career

Cria uma carreira.


📁 Estrutura Recomendada dos Projetos

DevLearning/
│
├── Models/                          → Projeto compartilhado entre todas as APIs
│   ├── Entities/
│   ├── DTOs/
│   └── Interfaces/
│
├── Students.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── Repository/
│   ├── Interfaces/
│   ├── Service/
│   └── Program.cs
│
├── Courses.Api/
│   ├── Controllers/
│   │   └── CoursesController.cs
│   ├── Data/
│   ├── Repository/
│   ├── Interfaces/
│   ├── Service/
│   └── Program.cs
│
├── Categories.Api/
│   ├── Controllers/
│   │   └── CategoriesController.cs
│   ├── Data/
│   ├── Repository/
│   ├── Interfaces/
│   ├── Service/
│   └── Program.cs
│
├── Career.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── Repository/
│   ├── Interfaces/
│   ├── Service/
│   └── Program.cs
│
├── Authors.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── Repository/
│   ├── Interfaces/
│   ├── Service/
│   └── Program.cs
│
└── docker-compose.yml

🧩 Descrição dos Projetos
🟦 1. Models (Compartilhado entre todas as APIs)

É um Shared Project ou Class Library, referenciado por todas as demais APIs.

Contém:

Models/
│
├── Entities/
│   ├── Student.cs
│   ├── Course.cs
│   ├── Category.cs
│   ├── Author.cs
│   ├── Career.cs
│   └── CareerItem.cs
│
└── DTOs/
    ├── StudentDto.cs
    ├── CourseDto.cs
    ├── AuthorDto.cs
    ├── CategoryDto.cs
    ├── CareerDto.cs
    └── CareerItemDto.cs


Funções:

Padroniza entidades entre microserviços

Evita duplicação de classes

Permite consistência na comunicação entre APIs

🟦 2. 🟦 2. Students.ApiStudents.Api

/DevLearningStudents.Api
│
├── Controllers/
│
├── Data/
│
├── Repository/
│   └── Interfaces/
│
├── Service/
│   └── Interfaces/
│
└── (usa Models compartilhado)

Consome: Courses API

Banco: SQL Server

🟦 3. DevLearningCourses.Api (inclui Category)

/DevLearningCourses.Api
│
├── Controllers/
│   ├── CoursesController.cs
│   └── CategoriesController.cs
│
├── Data/
│
├── Repository/
│   └── Interfaces/
│
├── Service/
│   └── Interfaces/
│
└── (usa Models compartilhado)


Consome: Authors API

Banco: SQL Server

🟦 4. DevLearningCareer.Api
/DevLearningCareer.Api
│
├── Controllers/
│
├── Data/
│
├── Repository/
│   └── Interfaces/
│
├── Service/
│   └── Interfaces/
│
└── (usa Models compartilhado)



Consome: Courses API

Banco: SQL Server

🟦 5. DevLearningAuthor.Api

/DevLearningAuthor.Api
│
├── Controllers/
│
├── Data/
│
├── Repository/
│   └── Interfaces/
│
├── Service/
│   └── Interfaces/
│
└── (usa Models compartilhado)



Banco: MongoDB

Fornece autores para Courses API

📝 Conclusão

Com a refatoração, a plataforma DevLearning passou a apresentar:

Menor acoplamento

Maior possibilidade de escalar partes independentes

Flexibilidade de tecnologias (SQL + MongoDB)

Comunicação clara entre serviços

Manutenção facilitada
