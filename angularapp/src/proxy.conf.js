const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/activities",
    ],
    target: "https://localhost:7049",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
