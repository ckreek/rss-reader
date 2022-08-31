import { makeAutoObservable, runInAction } from "mobx";
import { api } from "utils";

export interface Feed {
  id: number;
  name: string;
  url: string;
}

interface PostFeedDto {
  name: string;
  url: string;
}

export const allFeed: Feed = {
  id: 0,
  name: "All",
  url: "",
};

export class FeedStore {
  feeds: Feed[] = [];

  constructor() {
    makeAutoObservable(this);
  }

  async load() {
    const feeds = await api.get<Feed[]>("/feed");
    runInAction(() => {
      this.feeds = feeds;
    });
  }

  async add(dto: PostFeedDto) {
    const feed = await api.post<Feed>(`/feed`, dto);
    await this.load();
    return feed;
  }

  async update(feedId: number, dto: PostFeedDto) {
    await api.put<Feed>(`/feed/${feedId}`, dto);
    await this.load();
  }

  async delete(feedId: number) {
    if (feedId !== allFeed.id) {
      await api.del(`/feed/${feedId}`);
      await this.load();
    }
  }

  async restore(feedId: number) {
    if (feedId !== allFeed.id) {
      await api.post(`/feed/${feedId}/restore`);
      await this.load();
    }
  }

  getFeed(feedId: number) {
    if (feedId === 0) {
      return allFeed;
    }
    return this.feeds.find((x) => x.id === feedId);
  }
}
