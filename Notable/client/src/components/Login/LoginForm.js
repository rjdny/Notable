import React, { useState, useSyncExternalStore } from "react";
import { Button, Form, FormGroup, Label, Input, Dropdown } from "reactstrap";
import { useNavigate, Link } from "react-router-dom";
import { login } from "../../modules/authManager";

export default function LoginForm() {
  let navigate = useNavigate();

  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [error, setError] = useState("")

  const loginSubmit = (e) => {
    e.preventDefault();

    if(!email){
      setError("Please enter a valid Email.")
    }
    else if(!password){
      setError("Please enter a valid Password.")
    }
    else{
      login(email, password)
      .then(() => navigate("/"))
      .catch(() => setError("Incorrect Email or Password."));
    }
  };

  return (
    <Form className="cleanform" onSubmit={loginSubmit}>
        <div style={{marginLeft:"40%", paddingBottom:"20px"}}>
          <div style={{backgroundColor:"orange", width:"75px", borderRadius:"75px", padding:"5px", height:"75px", display:"block"}}>
            <img width={"65px"} height={"65px"} src="Icon.svg"/>
          </div>
        </div>
      <fieldset>
        <FormGroup>
          <Label for="email">Email</Label>
          <Input
            autoComplete="off"
            id="email"
            type="text"
            autoFocus
            onChange={(e) => setEmail(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="password">Password</Label>
          <Input
            autoComplete="off"
            id="password"
            type="password"
            onChange={(e) => setPassword(e.target.value)}
          />
        </FormGroup>
        <label className="authError">{error}</label>
        <FormGroup>
          <Button>Login</Button>
        </FormGroup>
        <em>
          Not registered? <Link to="/register">Register</Link>
        </em>
      </fieldset>
    </Form>
  );
}