/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["../Mona-Amiri/**/*.cshtml"],
  theme: {
    extend: {
      fontFamily: {
        yekanBakh: ["yekanbakh"],
      },
      screens: {
        md2: "900px",
        lg2: "1200px",
      },
    },
  },
  plugins: [require("@tailwindcss/forms")],
};
