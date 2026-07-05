import "./ScheduleCard.css";

export default function ScheduleCard({
  time,
  type,
  course,
  topic,
  activity,
  session,
}) {
  return (
    <div className={`schedule-card ${type}`}>
      <div className="card-time">{time}</div>

      <div className="card-type">
        {type === "class" ? "📘 CLASS" : "🎓 STUDY"}
      </div>

      <div className="card-course">{course}</div>

      {session && <div className="card-info">{session}</div>}

      {topic && <div className="card-info">{topic}</div>}

      {activity && <div className="card-activity">{activity}</div>}
    </div>
  );
}