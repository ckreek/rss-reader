const baseUrl = "https://localhost:7024/api";

const get = async <T>(url: string) => {
  const response = await fetch(baseUrl + url, {
    method: "GET",
  });
  const result = (await response.json()) as T;
  return result;
};

const post = async <T>(url: string, data?: object) => {
  const response = await fetch(baseUrl + url, {
    method: "POST",
    body: data && JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
  const result = (await response.json()) as T;
  return result;
};

export const api = {
  get,
  post,
};
