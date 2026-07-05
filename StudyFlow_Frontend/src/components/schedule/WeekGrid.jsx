import "./WeekGrid.css";
import ScheduleCard from "./ScheduleCard";

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

      {/* Day headers */}
      {week.days.map(day => (
        <div key={day.day} className="day-header">
          {day.day}
        </div>
      ))}

      {/* Day columns */}
      {week.days.map(day => (
        <div key={day.day} className="day-column">

          {day.blocks.length === 0 ? (
            <div className="empty-day">
              No activities
            </div>
          ) : (
            day.blocks.map((block, index) => (
              <ScheduleCard
                key={index}
                {...block}
              />
            ))
          )}

        </div>
      ))}

    </div>
  );
}