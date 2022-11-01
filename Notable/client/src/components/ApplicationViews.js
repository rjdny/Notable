import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import LoginForm from "./Login/LoginForm";
import RegisterForm from "./Login/RegisterForm";
import CategoryList from "./UserCategories/CategoryList";
import Explorer from "./UserExplorer/Explorer";
import NoteDetails from "./UserNotes/NoteDetails";
import NoteList from "./UserNotes/NoteList";

const ApplicationViews = ({isLoggedIn}) => {
    return (
        <Routes>
          {!isLoggedIn ? 
          <>
            <Route path="/">
              <Route index element={<Navigate to="/login" />} />
              <Route path="login" element={<LoginForm />} />
              <Route path="register" element={<RegisterForm />} />
            </Route>
          </> : <>
            <Route path="/">
              <Route index element={<Navigate to="/mynotes" />}/>
              <Route path="mynotes" element={<NoteList />}/>
              <Route path="notes/:noteId" element={<NoteDetails />}/>
              <Route path="mycategories" element={<CategoryList />}/>
              <Route path="explore" element={<Explorer />}/>
            </Route>
          </>}
          
          <Route path="*" element={<Navigate to={"/"} />}/>
      </Routes>
    );
  };
  
  export default ApplicationViews;