import { SnackbarProvider } from "notistack";
import { Feeds, FeedDetails, Posts } from "pages";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

const App = () => {
  return (
    <SnackbarProvider maxSnack={3}>
      <BrowserRouter>
        <Routes>
          <Route path="/feeds">
            <Route path="" element={<Feeds />} />
            <Route path=":feedId" element={<Feeds />} />
          </Route>
          <Route path="/feeds/create" element={<FeedDetails />} />
          <Route path="/feeds/:feedId/edit" element={<FeedDetails />} />
          <Route path="/feeds/:feedId/posts/:postId" element={<Posts />} />
          <Route path="*" element={<Navigate to="/feeds" replace />} />
        </Routes>
      </BrowserRouter>
    </SnackbarProvider>
  );
};

export default App;
