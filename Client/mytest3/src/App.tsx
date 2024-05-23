//import "bootstrap/dist/css/bootstrap.min.css";
//import "../src/Style/style.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { MainComponent } from "./Component/Main/MainPage";
import { FormAuto } from "./Component/Autorization/AuthWindow";
import { FormReg } from "./Component/Registration/RegWindow";
import { Profile } from "./Component/UserProfile/Profile";

export function App() {
  return (
    <BrowserRouter>
    <Routes>
      <Route path="/Autorization" element={<FormAuto />} />
      <Route path="/Registration" element={<FormReg />} />
      <Route path="/Profile" element={<Profile />} />
      <Route path="/" element={<MainComponent />} />
    </Routes>
    </BrowserRouter>
  );
}

export default App
