const API_URL = "http://localhost:5270/api";

export async function getProgramSchedule() {
  const response = await fetch(`${API_URL}/ProgramSchedule`);

  if (!response.ok) {
    throw new Error("Failed to fetch schedule");
  }

  return response.json();
}