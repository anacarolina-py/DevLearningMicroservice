📘 Course Microservice – API de Cursos

Este repositório contém o microserviço responsável pelo gerenciamento de cursos.
O código foi refatorado a partir de uma versão monolítica para uma arquitetura de microserviços, garantindo maior desacoplamento, escalabilidade e facilidade de manutenção.

Este serviço lida com todas as operações referentes a cursos, como criação, consulta, ativação, desativação e atualizações simples.

🚀 Base URL

Substitua em todas as rotas:

https://localhost:7242/api/courses

📚 Endpoints
📌 GET – Consultas
🔹 Listar todos os cursos
GET {{base_url_courses}}/all

🔹 Listar apenas cursos ativos
GET {{base_url_courses}}/

🔹 Buscar curso por ID
GET {{base_url_courses}}/{idDoCurso}

📌 POST – Criar um novo curso
POST {{base_url_courses}}/

📥 Body (raw – JSON):
{
  "tag": "Back-End Essentials",
  "title": "Dominando Otimização SQL",
  "summary": "Aprenda sobre índices, estatísticas, execution plans e como otimizar suas consultas.",
  "url": "sql-performance-masterclass",
  "durationInMinutes": 240,
  "level": 3,
  "free": false,
  "featured": false,
  "authorId": "COLOQUE_AQUI_O_ID_DO_AUTHOR",
  "categoryId": "COLOQUE_AQUI_O_ID_DA_CATEGORY",
  "tags": "SQL;Performance;DBA"
}

📌 PUT – Atualizações
🔹 Atualizar se o curso é free ou não
PUT {{base_url_courses}}/{idDoCurso}

Body (raw – JSON):
{
  "free": false
}

🔹 Ativar um curso (mudar active = true)
PUT {{base_url_courses}}/active/{idDoCurso}

📌 DELETE – Desativar um curso (active = false)
DELETE {{base_url_courses}}/{idDoCurso}

🧱 Observações Importantes

IDs de Author e Category devem existir nos microserviços correspondentes.

A atualização de free é simples e não altera outros campos.

A ativação/desativação não remove o curso do banco; apenas muda seu status.

O campo lastUpdateDate é atualizados automaticamente no backend.
