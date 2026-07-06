import "./ScheduleCard.css";



export default function ScheduleCard({
  time,
  type,
  course,
  topic,
  activity,
  session,
}) {
  const duration =
  (Number(time.split("-")[1].split(":")[0]) +
    Number(time.split("-")[1].split(":")[1]) / 60) -
  (Number(time.split("-")[0].split(":")[0]) +
    Number(time.split("-")[0].split(":")[1]) / 60);

  const isShort = duration <= 1;
  return (
    <div className={`schedule-card ${type}`}>
      <div className="card-time">{time}</div>

      <div className="card-type">
        {type === "class" ? "📘 CLASS" : "🎓 STUDY"}
      </div>

      <div className="card-course">{course}</div>

     {session && (
  <div className="card-info">
    {session}
  </div>
)}

{topic  && (
  <div className="card-info">
    {topic}
  </div>
)}

{activity && !isShort && (
  <div className="card-activity">
    {activity}
  </div>
)}
    </div>
  );
}