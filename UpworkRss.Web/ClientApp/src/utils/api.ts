const baseUrl = "https://localhost:7024/api";

const parseResponse = async (response: Response) => {
  const contentType = response.headers.get("content-type");
  if (contentType?.includes("application/json")) {
    const result = (await response.json());
    return result;
  }
};

const get = async <T>(url: string) => {
  const response = await fetch(baseUrl + url, {
    method: "GET",
  });
  return await parseResponse(response) as T;
};

const post = async <T>(url: string, data?: object) => {
  const response = await fetch(baseUrl + url, {
    method: "POST",
    body: data && JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
  return await parseResponse(response) as T;
};

const patch = async <T>(url: string, data?: object) => {
  const response = await fetch(baseUrl + url, {
    method: "PATCH",
    body: data && JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
  return await parseResponse(response) as T;
};

export const api = {
  get,
  post,
  patch,
};
