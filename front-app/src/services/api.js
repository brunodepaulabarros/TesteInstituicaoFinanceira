import { create } from "apisauce";

const Api = create({
  baseURL: "https://localhost:44330/api",
  timeout:10000,
});

export default Api;