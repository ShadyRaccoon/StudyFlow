import "./WeekGrid.css";
import ScheduleCard from "./ScheduleCard";
import { Fragment } from "react";


const hours = [
  "08:00","09:00","10:00","11:00","12:00",
  "13:00","14:00","15:00","16:00","17:00","18:00"
];

const START_HOUR = 8;
const HOUR_HEIGHT = 90;

function getTop(time) {
  const [start] = time.split("-");
  const [h, m] = start.split(":").map(Number);

  return ((h - START_HOUR) + m / 60) * HOUR_HEIGHT;
}

function getHeight(time) {
  const [start, end] = time.split("-");

  const [sh, sm] = start.split(":").map(Number);
  const [eh, em] = end.split(":").map(Number);

  const hours =
    (eh + em / 60) -
    (sh + sm / 60);

  return hours * HOUR_HEIGHT;
}

export default function WeekGrid({ schedule, currentWeek }) {
  if (!schedule) {
    return (
      <div className="loading">
        <div className="spinner"></div>
        <h2>Loading schedule...</h2>
      </div>
    );
  }

  const week = schedule.weeks[currentWeek];

  return (
    <div className="calendar">

      {/* top-left empty cell */}
      <div className="corner"></div>

      {/* Day headers */}
      {week.days.map(day => (
        <div key={day.day} className="day-header">
          {day.day}
        </div>
      ))}

      {/* Time column */}
      <div className="time-column">
        {hours.map(hour => (
          <div key={hour} className="time-slot">
            {hour}
          </div>
        ))}
      </div>

       {/* Day columns */}
      {week.days.map(day => (
        <div key={day.day} className="day-column">

          {day.blocks.map((block, index) => (
            <div
              key={index}
              className="event-wrapper"
              style={{
                top: getTop(block.time),
                height: Math.max(getHeight(block.time), 120)
              }}
            >
              <ScheduleCard {...block} />
            </div>
          ))}
        </div>
      ))}
    </div>
  );
}