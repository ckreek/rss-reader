const baseUrl = `${process.env.REACT_APP_API_URL}/api`;

const getQuery = <D extends Record<string, any>>(data?: D): string => {
  if (!data || Object.keys(data).length === 0) {
    return "";
  }

  const str = [];
  for (const p in data) {
    if (data.hasOwnProperty(p)) {
      str.push(encodeURIComponent(p) + "=" + encodeURIComponent(data[p]));
    }
  }
  return "?" + str.join("&");
};

const parseResponse = async (response: Response) => {
  const contentType = response.headers.get("content-type");
  if (contentType?.includes("application/json")) {
    const result = await response.json();
    return result;
  }
};

const get = async <T, D = unknown>(url: string, data?: D) => {
  const response = await fetch(baseUrl + url + getQuery(data), {
    method: "GET",
  });
  return (await parseResponse(response)) as T;
};

const post = async <T>(url: string, data?: object) => {
  const response = await fetch(baseUrl + url, {
    method: "POST",
    body: data && JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
  return (await parseResponse(response)) as T;
};

const patch = async <T>(url: string, data?: object) => {
  const response = await fetch(baseUrl + url, {
    method: "PATCH",
    body: data && JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
  return (await parseResponse(response)) as T;
};

const put = async <T>(url: string, data?: object) => {
  const response = await fetch(baseUrl + url, {
    method: "PUT",
    body: data && JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
  return (await parseResponse(response)) as T;
};

const del = async <T>(url: string) => {
  const response = await fetch(baseUrl + url, {
    method: "DELETE",
  });
  return (await parseResponse(response)) as T;
};

export const api = {
  get,
  post,
  patch,
  put,
  del,
};
