import "./Header.css";

export default function Header({
  currentWeek,
  totalWeeks,
  previousWeek,
  nextWeek,
}) {
  return (
    <header className="header">
      <button
        onClick={previousWeek}
        disabled={currentWeek === 0}
      >
        ←
      </button>

      <h1>
        Week {currentWeek + 1}
        {totalWeeks > 0 && ` / ${totalWeeks}`}
      </h1>

      <button
        onClick={nextWeek}
        disabled={currentWeek === totalWeeks - 1}
      >
        →
      </button>
    </header>
  );
}