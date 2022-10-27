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