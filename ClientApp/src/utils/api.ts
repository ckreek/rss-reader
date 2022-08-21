const get = async <T>(url: string) => {
  const response = await fetch("https://localhost:7024/api" + url, {
    method: "GET",
  });
  const data = await response.json() as T;
  return data;
};

export const api = {
  get,
};
