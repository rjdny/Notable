import { getToken, _getUserProfile } from "./authManager";

const _apiUrl = "/api/note";


export const _getMyNotes = () => {
    return _getUserProfile().then((up) => {
        return getToken().then((token) =>
        fetch(`${_apiUrl}/usernotes/${up.id}`, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`
          }
        }).then(resp => resp.json()));
    })
};

export const _getNoteById = (noteId) => {
    return _getUserProfile().then((up) => {
        return getToken().then((token) =>
        fetch(`${_apiUrl}/${noteId}`, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`
          }
        }).then(resp => resp.json()));
    })
};

export const _updateNote = (noteId, note) => {
    return getToken().then((token) =>
        fetch(_apiUrl + `/${noteId}`, {
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
            method: "PUT",
            body:JSON.stringify(note)
    }))
}

export const _addNote = (note) => {
    return getToken().then((token) =>
        fetch(_apiUrl, {
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": 'application/json'
            },
            method: "POST",
            body:JSON.stringify(note)
    }))
}

export const _deleteNote = (noteId) => {
        return getToken().then((token) =>
        fetch(`${_apiUrl}/${noteId}`, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
            method: "DELETE",
    }))
}