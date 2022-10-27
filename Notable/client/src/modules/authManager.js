import firebase from "firebase/app";
import "firebase/auth";

const _apiUrl = "/api/userprofile";

export const _getUserProfile = () =>{
  let user = firebase.auth().currentUser

  if(!user){
    return new Promise(() => {return {}})
  }

  return getToken().then((token) =>
  fetch(`${_apiUrl}/getbyfirebaseid/${user.uid}`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${token}`
    }
  }).then(resp => resp.json()));
}

const _doesUserExist = (firebaseUserId) => {
  return getToken().then((token) =>
    fetch(`${_apiUrl}/DoesUserExist/${firebaseUserId}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    }).then(resp => resp.ok));
};

const _saveUser = (userProfile) => {
  return getToken().then((token) =>
    fetch(_apiUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(userProfile)
    }).then(resp => resp.json()));
};

export const getToken = () => {
  const currentUser = firebase.auth().currentUser;
  if (!currentUser) {
    throw new Error("Cannot get current user. Did you forget to login?");
  }
  return currentUser.getIdToken();
};

export const login = (email, pw) => {
  return firebase.auth().signInWithEmailAndPassword(email, pw)
    .then((signInResponse) => _doesUserExist(signInResponse.user.uid))
    .then((doesUserExist) => {
      if (!doesUserExist) {
        console.log("User was not")

        logout();

        throw new Error("Something's wrong. The user exists in firebase, but not in the application database.");
      } else {
        _onLoginStatusChangedHandler(true);
      }
    }).catch(err => {
      console.error(err);
      throw err;
    });
};


export const logout = () => {
  firebase.auth().signOut()
};


export const register = (userProfile, password) => {
  return firebase.auth().createUserWithEmailAndPassword(userProfile.email, password)
    .then((createResponse) => _saveUser({
      ...userProfile,
      firebaseUserId: createResponse.user.uid
    }).then(() => _onLoginStatusChangedHandler(true)));
};

let _onLoginStatusChangedHandler = () => {
  throw new Error("There's no login status change handler. Did you forget to call 'onLoginStatusChange()'?")
};

export const onLoginStatusChange = (onLoginStatusChangedHandler) => {
  const unsubscribeFromInitialLoginCheck =
    firebase.auth().onAuthStateChanged(function initialLoadLoginCheck(user) {
      unsubscribeFromInitialLoginCheck();
      onLoginStatusChangedHandler(!!user);

      firebase.auth().onAuthStateChanged(function logoutCheck(user) {
        if (!user) {
          onLoginStatusChangedHandler(false);
        }
      });
    });
  _onLoginStatusChangedHandler = onLoginStatusChangedHandler;
};