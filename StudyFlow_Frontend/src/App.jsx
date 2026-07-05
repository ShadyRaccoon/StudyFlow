import { useEffect, useState } from "react";
import "./App.css";

import Sidebar from "./components/layout/Sidebar";
import Header from "./components/layout/Header";
import WeekGrid from "./components/schedule/WeekGrid";

import mockSchedule from "./mockSchedule";

import { getProgramSchedule } from "./services/api";

function App() {
  const [schedule, setSchedule] = useState(null);
  const [currentWeek, setCurrentWeek] = useState(0);
  const [error, setError] = useState("");

  useEffect(() => {
    async function loadSchedule() {
      try { 
        // Not working with the API, using mock data for now
        
        //const data = await getProgramSchedule();

        //let json = data.generatedJSON
        //  .replace(/^```json\s*/i, "")
        //  .replace(/```$/, "");

        //const parsedSchedule = JSON.parse(json);

        //setSchedule(parsedSchedule);
        // Using mock data
        setSchedule(mockSchedule);
      } catch (err) {
        console.error(err);
        setError("Couldn't load schedule.");
      }
    }

    loadSchedule();
  }, []);

  if (error) {
    return (
      <div className="loading">
        <h2>{error}</h2>
      </div>
    );
  }

  return (
    <div className="app">
      <Sidebar />

      <main className="content">
        <Header
          currentWeek={currentWeek}
          totalWeeks={schedule?.weeks.length || 0}
          previousWeek={() =>
            setCurrentWeek((w) => Math.max(0, w - 1))
          }
          nextWeek={() =>
            setCurrentWeek((w) =>
              Math.min(schedule.weeks.length - 1, w + 1)
            )
          }
        />

        <WeekGrid
          schedule={schedule}
          currentWeek={currentWeek}
        />
      </main>
    </div>
  );
}

export default App;