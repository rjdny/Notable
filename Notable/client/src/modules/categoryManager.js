import { getToken } from "./authManager";

const _apiUrl = "/api/category";


export const _getAllCategories = () => {
    return getToken().then((token) =>
    fetch(`${_apiUrl}`, {
        method: "GET",
        headers: {
        Authorization: `Bearer ${token}`
        }
    }).then(resp => resp.json()));
};

export const _addCategory = (name) => {
    return getToken().then((token) =>
    fetch(`${_apiUrl}`, {
        method: "POST",
        headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": 'application/json'
        },
        body:(JSON.stringify({"name":name}))
    }).then(resp => resp.json()));
}


export const _deleteCategory = (categoryId) =>{
    return getToken().then((token) =>
    fetch(`${_apiUrl}/${categoryId}`, {
        method: "DELETE",
        headers: {
            Authorization: `Bearer ${token}`
        }
    }))
}


