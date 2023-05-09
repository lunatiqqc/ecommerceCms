/** @type {import('next').NextConfig} */
const nextConfig = {
    experimental: { instrumentationHook: true },
    output: "export",
};

module.exports = nextConfig;
