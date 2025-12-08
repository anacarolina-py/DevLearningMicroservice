🚀 DevLearning – Career Microservice

Este projeto corresponde à extração dos módulos Career e CareerItem de um monolito existente, transformando-os em um microserviço independente dentro do ecossistema DevLearning.

O objetivo principal da tarefa foi desacoplar funcionalidades, eliminar dependências diretas entre módulos e permitir que cada parte do sistema evolua, escale e seja implantada de forma isolada.

📦 Contexto do Projeto

O sistema original era um monolito onde todos os módulos compartilhavam:

O mesmo banco de dados

O mesmo domínio de entidades

A mesma camada de serviços

Joins diretos entre tabelas

Injeção de dependência entre serviços internos

Com a migração para microserviços, as entidades foram separadas em contextos próprios.
Este repositório representa o microserviço responsável por:

Gerenciar Carreiras (Career)

Gerenciar Itens de Carreira (CareerItem)

🔄 Principais Mudanças na Arquitetura
1️⃣ Separação dos bancos e eliminação de joins diretos

Antes (monolito):
O CareerService fazia joins com a tabela Course para trazer o nome do curso relacionado ao CareerItem.

Exemplo típico:

SELECT Career.*, CareerItem.*, Course.Name
FROM CareerItem
JOIN Course ON Course.Id = CareerItem.CourseId


Agora (microserviço):
Cada microserviço possui seu próprio banco isolado, logo joins entre tabelas de serviços diferentes não são mais possíveis.

Para resolver isso:

✔ Foi implementado uso de HttpClient para consumir o microserviço de Course
✔ O nome do curso agora é obtido através de uma requisição externa, e não mais via join

2️⃣ Validação de existência do Course ao criar CareerItem

Antes (monolito):
O CareerItemService injetava o CourseService diretamente:

public CareerItemService(ICourseService courseService) { ... }


Isso permitia validar facilmente:

var course = _courseService.GetCourseById(courseId);


Agora (microserviços):
Como Career e Course não ficam mais no mesmo projeto:

❌ Não existe mais injeção de dependência entre serviços internos
✔ Agora a validação usa HttpClient para consultar o microserviço de Course:

var course = await _courseApiClient.GetCourseByIdAsync(courseId);
if (course is null)
    throw new Exception("Course not found");


Essa alteração garante autonomia e isolamento entre os microserviços, mantendo ainda a integridade dos dados.

3️⃣ Remoção das referências diretas entre domínios

Antes era possível referenciar a entidade Course dentro de CareerItem.
Agora CareerItem guarda apenas o CourseId, e qualquer informação adicional (como o nome do curso) é buscada externamente.

🧩 Como o Microserviço Funciona
Endpoints principais
Career

GET /api/career

GET /api/career/{id}

POST /api/career

PUT /api/career/ChanceActive/{id}

PUT /api/career/Update/{id}



CareerItem

GET /api/career/{careerId}/items

POST /api/career/{careerId}/items

DELETE /api/careeritem/career/{id}

DELETE /api/careeritem/course/{id}
DELETE /api/careeritem/careercourse/{id}/{id}

No POST de CareerItem:

Recebe CourseId

Faz requisição ao microserviço de Course

Valida se o curso existe

Salva o CareerItem no banco local

Opcionalmente, consulta novamente o Course para retornar o nome no DTO de resposta

🕸️ Comunicação entre Microserviços

A comunicação entre Career API e Course API é feita por:

✔ HttpClient configurado via Typed Client
✔ Base address injetada via IConfiguration
✔ Tratamento de erros + fallback simples

Esse padrão é o mais recomendado em .NET para comunicação síncrona entre microserviços.

🧱 Tecnologias Utilizadas

.NET 9 / ASP.NET Core

Dapper

SQL Serve

HttpClient Typed Clients

Docker (opcional)


Clean Architecture/DDD (parcial)

📚 Resumo da Transformação
Mecanismo	Antes (Monolito)	Depois (Microserviço)
Banco de Dados	Único banco compartilhado	Banco isolado por serviço
Validação de Course	Injeção do CourseService	HttpClient → chamada externa
Joins	Joins diretos entre tabelas	Busca externa via API de Course
Dependências	Fortemente acoplado	Totalmente desacoplado
Deploy	Integração total	Independente
✅ Conclusão

A separação de Career e CareerItem em seu próprio microserviço trouxe:

Independência do domínio

Autonomia de deploy

Maior desacoplamento

Comunicação via API externa

Remoção de dependências fortes entre serviços
