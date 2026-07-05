const mockSchedule = {
  weeks: [
    {
      week: 1,
      days: [
        {
          day: "Monday",
          blocks: [
            {
              time: "08:00-10:00",
              type: "class",
              course: "Fullstack Development",
              session: "Course",
            },
            {
              time: "10:30-12:00",
              type: "study",
              course: "Fullstack Development",
              topic: "JavaScript Fundamentals",
              activity: "REST API Assignment",
            },
          ],
        },
        {
          day: "Tuesday",
          blocks: [
            {
              time: "09:00-11:00",
              type: "study",
              course: "Web Programming",
              topic: "CSS Layouts",
              activity: "Quiz Prep",
            },
          ],
        },
        {
          day: "Wednesday",
          blocks: [],
        },
        {
          day: "Thursday",
          blocks: [],
        },
        {
          day: "Friday",
          blocks: [],
        },
        {
          day: "Saturday",
          blocks: [],
        },
        {
          day: "Sunday",
          blocks: [],
        },
      ],
    },
    {
      week: 2,
      days: [
        {
          day: "Monday",
          blocks: [
            {
              time: "14:00-16:00",
              type: "class",
              course: "Academic Ethics",
              session: "Course",
            },
          ],
        },
        {
          day: "Tuesday",
          blocks: [
            {
              time: "10:00-12:00",
              type: "study",
              course: "Web Programming",
              topic: "React Components",
              activity: "Midterm",
            },
          ],
        },
        {
          day: "Wednesday",
          blocks: [],
        },
        {
          day: "Thursday",
          blocks: [],
        },
        {
          day: "Friday",
          blocks: [],
        },
        {
          day: "Saturday",
          blocks: [],
        },
        {
          day: "Sunday",
          blocks: [],
        },
      ],
    },
  ],
};

export default mockSchedule;