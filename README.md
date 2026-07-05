# StudyFlow API Documentation

Base URL: `http://localhost:5270`  
Interactive docs: `http://localhost:5270/scalar/v1`

---

## Setup

Run `_SETUP.sh` from the repo root. It installs .NET, restores packages, sets up your Anthropic API key, and initialises the database.

Once setup is done:
```bash
cd StudyFlow_API
dotnet run
```

---

## Initialisation Flow

Before the schedule exists, these two endpoints need to be called once in order:

```
POST /api/Schedule/process-materials   ← reads course PDFs, extracts metadata via Claude Haiku (~2 min)
POST /api/Schedule/generate            ← generates the 2-week schedule from that metadata (~10 sec)
```

Both are protected — calling them again after they've already run returns `409 Conflict` with a message explaining why. To reset, use the admin endpoints.

---

## Endpoints

### Courses

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Course` | All courses |
| GET | `/api/Course/{id}` | Single course by ID |
| POST | `/api/Course` | Add a course |

**GET /api/Course response:**
```json
[
  { "id": 1, "name": "DevOps" },
  { "id": 2, "name": "Fullstack Development" },
  { "id": 3, "name": "Academic Ethics" },
  { "id": 4, "name": "Web Programming" }
]
```

---

### Class Sessions

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/ClassSession` | All sessions |
| GET | `/api/ClassSession/course/{courseId}` | Sessions for a specific course |
| POST | `/api/ClassSession` | Add a session |

**GET /api/ClassSession response:**
```json
[
  {
    "id": 1,
    "courseId": 2,
    "course": { "id": 2, "name": "Fullstack Development" },
    "sessionType": "Course",
    "dayOfWeek": "Tuesday",
    "startTime": "08:00:00",
    "endTime": "10:00:00"
  }
]
```

`sessionType` values: `"Course"`, `"Laboratory"`, `"Seminar"`

---

### Activities

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Activity` | All activities |
| GET | `/api/Activity/course/{courseId}` | Activities for a specific course |
| POST | `/api/Activity` | Add an activity |

**GET /api/Activity response:**
```json
[
  {
    "id": 1,
    "courseId": 2,
    "course": { "id": 2, "name": "Fullstack Development" },
    "title": "REST API Assignment",
    "type": "Homework",
    "dueDate": "2025-03-20T00:00:00",
    "estimatedEffort": 3
  }
]
```

`type` values: `"Exam"`, `"Homework"`, `"Project"`, `"Quiz"`  
`estimatedEffort`: integer 1–5 (1 = light, 5 = very heavy)

---

### Course Materials

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/CourseMaterial` | All materials |
| GET | `/api/CourseMaterial/{id}` | Single material by ID |
| GET | `/api/CourseMaterial/course/{courseId}` | Materials for a specific course |
| POST | `/api/CourseMaterial` | Add a material |
| PUT | `/api/CourseMaterial/{id}` | Update a material |

**GET /api/CourseMaterial response:**
```json
[
  {
    "id": 1,
    "courseId": 2,
    "classSessionId": 1,
    "title": "JavaScript",
    "blobRef": "../Database/Materials/Fullstack/Course 2 - JavaScript.pdf",
    "extractedTopic": "JavaScript programming language fundamentals",
    "extractedDifficulty": 3,
    "estimatedStudyTime": 180
  }
]
```

`extractedTopic`, `extractedDifficulty`, `estimatedStudyTime` are `null` until `process-materials` has been called.  
`estimatedStudyTime` is in minutes.

---

### Program Schedule

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/ProgramSchedule` | Get the latest generated schedule |
| POST | `/api/ProgramSchedule` | Store a schedule manually |

**GET /api/ProgramSchedule response:**
```json
{
  "id": 1,
  "generatedJSON": "{ ... }",
  "generatedAt": "2026-07-05T15:32:10"
}
```

`generatedJSON` is the full schedule as a JSON string. Parse it to get the schedule structure below.

**Schedule JSON structure:**
```json
{
  "weeks": [
    {
      "week": 1,
      "days": [
        {
          "day": "Monday",
          "blocks": [
            {
              "time": "14:00-16:00",
              "type": "class",
              "course": "Academic Ethics",
              "session": "Course"
            },
            {
              "time": "16:00-17:00",
              "type": "study",
              "course": "Web Programming",
              "topic": "Web Programming Overview",
              "activity": "CSS Layout Quiz Prep"
            }
          ]
        }
      ]
    }
  ]
}
```

`type` is either `"class"` or `"study"`.  
Class blocks have `session` (the session type). Study blocks have `topic` and `activity`.

---

### Schedule (AI Triggers)

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Schedule/process-materials` | Run Stage 1 — extract metadata from PDFs |
| POST | `/api/Schedule/generate` | Run Stage 2 — generate schedule from metadata |

Both return `200 OK` on success and `409 Conflict` if already done.

---

### Admin

| Method | Endpoint | Description |
|--------|----------|-------------|
| DELETE | `/api/Admin/wipe` | Wipe the entire database |
| POST | `/api/Admin/reseed` | Wipe and reseed from JSON files |

Use `reseed` when you want to start fresh. After reseed, you'll need to call `process-materials` and `generate` again.

---

## Typical Frontend Flow

```
1. GET /api/ProgramSchedule
   → if 200, parse generatedJSON and render the schedule
   → if 404, the schedule hasn't been generated yet

2. POST /api/Schedule/process-materials   (admin/setup step, not user-facing)
   → 200: materials processed
   → 409: already done, skip

3. POST /api/Schedule/generate            (admin/setup step, not user-facing)
   → 200: schedule ready
   → 409: already exists, skip

4. GET /api/ProgramSchedule
   → parse generatedJSON, render weekly view
```

---

## Notes

- CORS is open — all origins, methods and headers are allowed.
- The database is SQLite, stored at `Database/studyflow.db`. It persists across restarts.
- The Anthropic API key is stored in .NET user secrets — never committed to git.
- `process-materials` takes ~2 minutes for 20 PDFs. Don't call it from the UI.
- `generate` takes ~10 seconds. Safe to call from an admin panel or setup screen.