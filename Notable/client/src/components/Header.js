import React, { useEffect, useState } from "react";
import { NavLink as RRNavLink, useNavigate } from "react-router-dom";
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
} from "reactstrap";
import { logout, _getUserProfile } from "../modules/authManager";

export default function Header({ isLoggedIn }) {
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);


  return (
    <div>
      <Navbar className="navtext" color="dark" dark expand="md">
        <NavbarBrand tag={RRNavLink} to="/">
        <div className="zoom">
            <img width={"50px"} height={"50px"} src="/Icon.svg"/>Notable
        </div>
        </NavbarBrand>
        <NavbarToggler onClick={toggle} />
        <Collapse isOpen={isOpen} navbar>
          <Nav className="mr-auto" navbar>
            {isLoggedIn && (
              <>
                <NavItem className="Navvy">
                  <NavLink tag={RRNavLink} to="/mynotes">
                  Notes
                  </NavLink>
                </NavItem>
                <NavItem className="Navvy">
                  <NavLink tag={RRNavLink} to="/mycategories">
                  Categories
                  </NavLink>
                </NavItem>
                <NavItem className="Navvy">
                  <NavLink tag={RRNavLink} to="/explore">
                    Explore
                  </NavLink>
                </NavItem>

                <NavItem>
                   <NavLink tag={RRNavLink} to="/login" onClick={(e) => {logout();}}>
                    Logout
                  </NavLink>
                </NavItem>
              </>
            )}
            {!isLoggedIn && (
              <>
                <NavItem className="Navvy">
                  <NavLink tag={RRNavLink} to="/login">
                    Login
                  </NavLink>
                </NavItem>
                <NavItem className="Navvy">
                  <NavLink tag={RRNavLink} to="/register">
                    Register
                  </NavLink>
                </NavItem>
              </>
            )}
          </Nav>
        </Collapse>
      </Navbar>
    </div>
  );
}