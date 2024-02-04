import { defineConfig, DefaultTheme } from "vitepress";
import exportedApi from "./exportedApi";
import pkg from "../package.json";

const sidebarApi = exportedApi.map((ns) => {
  return {
    text: ns.name,
    items: ns.members.map((type) => {
      const typeUriName = type.uriName || type.name;

      return {
        text: type.name,
        link: `/api/${ns.name}/${typeUriName}`,
      };
    }),
  };
});

export default defineConfig({
  title: "Dvchevskii.Result",
  description: "Dvchevskii.Result oackage documentation",
  base: "/result/",
  assetsDir: "public",
  appearance: "force-dark",
  lang: "en-US",
  head: [["link", { rel: "icon", href: "/result/favicon.ico" }]],
  themeConfig: {
    nav: [
      { text: "Guide", link: "/guide/installation" },
      { text: "API", link: "/api/" },
      {
        text: "v1.0.0",
        items: [
          {
            text: "Release notes",
            link: "https://github.com/2chevskii/result/releases/tag/v1.0.0",
          },
          { text: "Changelog", link: "https://keepachangelog.com/en/1.0.0" },
        ],
      },
    ],

    sidebar: [
      {
        text: "Guide",
        items: [
          {
            text: "Installation",
            link: "/guide/installation",
          },
          {
            text: "Usage",
            link: "/guide/usage",
          },
        ],
      },
      {
        text: "API",
        link: "/api/",
        items: sidebarApi,
      },
    ],

    socialLinks: [
      { icon: "github", link: "https://github.com/2chevskii/result" },
    ],

    footer: {
      message: `Built with <a href="https://vitepress.dev">VitePress</a> | API version: <code>${pkg["version"]}</code><br/>Released under the MIT license. All rights reserved`,
    },
    search: { provider: "local" },
  },
  markdown: {
    theme: "dark-plus",
  },
});
