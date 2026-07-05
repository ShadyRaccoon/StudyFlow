import "./Sidebar.css";

export default function Sidebar() {
  return (
    <aside className="sidebar">
      <h2 className="logo">📚 StudyFlow</h2>

      <nav>
        <button className="active">
          📅 Schedule
        </button>

        <button>
          📚 Courses
        </button>

        <button>
          📄 Materials
        </button>
      </nav>
    </aside>
  );
}