# 📚 Category Microservice — Documentação da Refatoração

Este documento descreve a implementação e melhorias realizadas no módulo **Category**. Este módulo é responsável por gerenciar categorias, incluindo operações de CRUD e verificações de integridade antes de exclusões.

---

## 🏗️ Contexto do Módulo

O módulo Category organiza e gerencia as categorias da plataforma. As principais responsabilidades incluem:

- Listar, criar, atualizar e deletar categorias.
- Garantir que não seja possível deletar categorias que possuam cursos associados.

Durante esta implementação:

- Foi adicionado o método `HasCourseAsync` para verificar cursos associados antes de deletar uma categoria.
- Endpoints e serviços foram mantidos simples, seguindo boas práticas de DDD.

---

## 🔄 Principais Funcionalidades

### 1. 🗄️ CRUD de Categorias

**Endpoints disponíveis:**

| Método | Endpoint                  | Descrição                                      |
|--------|---------------------------|------------------------------------------------|
| GET    | `/api/Categories`         | Lista todas as categorias.                    |
| GET    | `/api/Categories/{id}`    | Retorna uma categoria específica pelo ID.     |
| POST   | `/api/Categories`         | Cria uma nova categoria.                      |
| PUT    | `/api/Categories/{id}`    | Atualiza uma categoria existente.            |
| DELETE | `/api/Categories/{id}`    | Deleta uma categoria (apenas se não houver cursos associados). |

---

### 2. 🔍 Verificação de Cursos Associados

Foi adicionado o método `HasCourseAsync`:

```csharp
public async Task DeleteCategoryAsync(Guid id)
{
    if(await _categoryRepository.HasCourseAsync(id))
    {
        throw new ArgumentException("Não é possível deletar uma categoria que possui cursos associados");
    }

    await _categoryRepository.DeleteCategoryAsync(id);
}

```

Funcionalidade:
- Antes de deletar uma categoria, o sistema verifica se há cursos vinculados. Se houver, a exclusão é bloqueada para manter a integridade dos dados.

## 🔧 Sugestões de Melhorias Futuras

### Verificação de Duplicidade ao Criar

Antes de criar uma categoria, verificar se já existe uma com o mesmo nome ou URL.

### Endpoint para Listar Cursos por Categoria

Criar ```/api/Categories/{id}/Courses``` para retornar todos os cursos de uma categoria específica.

## Validação de Dados

Garantir que campos obrigatórios estejam preenchidos e valores de texto respeitem limites máximos.