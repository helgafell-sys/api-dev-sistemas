# 🏥 Consultório - Sistema de Gerenciamento de Pacientes

Sistema completo com **API REST** e **GUI (WPF)** para gerenciamento de pacientes e consultas.

## 📋 Requisitos

- **.NET 9.0**
- **SQL Server / SQLite** (padrão)
- **Visual Studio 2022** ou VS Code

## 🚀 Como Executar

### 1. Preparar o Banco de Dados

```bash
cd Consultorio
dotnet ef database update
```

### 2. Iniciar a API

```bash
dotnet run --urls "http://localhost:5000"
```

### 3. Iniciar a GUI (WPF)

```bash
cd Consultorio.Desktop
dotnet run
```

## 📊 Estrutura do Banco de Dados

### Tabela: Patients
| Campo | Tipo | Restrições |
|-------|------|-----------|
| Id | int | PK |
| Name | string(100) | NOT NULL, UNIQUE |
| Email | string(150) | NOT NULL, UNIQUE |
| CreatedAt | datetime | Default: GETDATE() |

### Tabela: Appointments
| Campo | Tipo | Restrições |
|-------|------|-----------|
| Id | int | PK |
| AppointmentDate | datetime | NOT NULL |
| Reason | string(500) | NOT NULL |
| PatientId | int | FK → Patients(Id) |

**Relacionamento**: `Patient 1:N Appointment` (INNER JOIN)

## 🔌 Rotas da API

### Pacientes

#### GET - Listar todos os pacientes
```http
GET /api/v1/patients HTTP/1.1
Host: localhost:5000
```

**Resposta (200):**
```json
[
  {
    "id": 1,
    "name": "João Silva",
    "email": "joao@email.com"
  }
]
```

#### GET - Buscar paciente por ID
```http
GET /api/v1/patients/1 HTTP/1.1
Host: localhost:5000
```

**Resposta (200):**
```json
{
  "id": 1,
  "name": "João Silva",
  "email": "joao@email.com"
}
```

#### POST - Criar paciente
```http
POST /api/v1/patients HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
  "name": "Maria Santos",
  "email": "maria@email.com"
}
```

**Resposta (201):**
```json
{
  "id": 2,
  "name": "Maria Santos",
  "email": "maria@email.com"
}
```

#### PUT - Atualizar paciente
```http
PUT /api/v1/patients/1 HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
  "name": "João Silva Atualizado",
  "email": "joao.novo@email.com"
}
```

**Resposta (204):** No Content

#### DELETE - Remover paciente
```http
DELETE /api/v1/patients/1 HTTP/1.1
Host: localhost:5000
```

**Resposta (204):** No Content

### Consultas (Appointments)

#### GET - Listar todas as consultas (com JOIN)
```http
GET /api/v1/appointments HTTP/1.1
Host: localhost:5000
```

**Resposta (200):**
```json
[
  {
    "id": 1,
    "appointmentDate": "2024-12-10T14:30:00",
    "reason": "Consulta de rotina",
    "patientId": 1,
    "patientName": "João Silva"
  }
]
```

#### POST - Agendar consulta
```http
POST /api/v1/appointments HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
  "appointmentDate": "2024-12-10T14:30:00",
  "reason": "Consulta de rotina",
  "patientId": 1
}
```

## 🧪 Testando com Postman

1. Abra o **Postman**
2. Importe as rotas acima
3. Execute os testes

## 📱 Interface Gráfica (WPF)

### Funcionalidades
- ✅ Listar pacientes
- ✅ Criar novo paciente
- ✅ Editar paciente selecionado
- ✅ Deletar paciente
- ✅ Validações em tempo real
- ✅ Mensagens de feedback

## 🛠️ Tecnologias Utilizadas

- **Backend**: ASP.NET Core 9.0
- **Frontend**: WPF + MVVM Toolkit
- **Banco de Dados**: Entity Framework Core 9.0 + SQLite
- **Validação**: DataAnnotations
- **API**: RESTful com Controllers

## 📄 Estrutura de Pastas

```
├── Models/
│   ├── Patient.cs
│   ├── Appointment.cs
│   ├── PatientDtos.cs
│   └── AppointmentDtos.cs
├── Controllers/
│   ├── PatientsController.cs
│   └── AppointmentsController.cs
├── Data/
│   ├── AppDbContext.cs
│   └── DesignTimeDbContextFactory.cs
├── Consultorio.Desktop/
│   ├── ViewModels/
│   │   └── PatientsViewModel.cs
│   ├── Views/
│   │   ├── MainWindow.xaml
│   │   └── MainWindow.xaml.cs
│   └── Services/
│       └── ApiClient.cs
└── Program.cs
```

## 🐛 Troubleshooting

**Erro: "Cannot connect to localhost:5000"**
- Verifique se a API está rodando
- Altere a porta em `App.xaml.cs`

**Erro: "Migration not found"**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 👨‍💻 Autor

Desenvolvido como projeto de conclusão da disciplina de Desenvolvimento de Sistemas.

---

**Status**: ✅ Completo e Funcional
