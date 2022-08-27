import { Feeds, FeedsCreate } from "pages";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/feeds" element={<Feeds />} />
        <Route path="/feeds/create" element={<FeedsCreate />} />
        <Route path="*" element={<Navigate to="/feeds" replace />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
