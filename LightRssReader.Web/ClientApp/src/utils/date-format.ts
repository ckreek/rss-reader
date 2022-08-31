import moment from "moment";

export const formatDate = (dateStr: string) => {
  const date = moment(new Date(dateStr));
  return `${date.format("D MMMM")}, ${date.fromNow()}`;
};
