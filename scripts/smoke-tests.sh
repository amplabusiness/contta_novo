#!/usr/bin/env bash
set -euo pipefail
: "${BASE_URL_SEARCH:?defina BASE_URL_SEARCH}"  # ex: https://contta-searchapi-staging.onrender.com

# health
code=$(curl -s -o /dev/null -w "%{http_code}" "$BASE_URL_SEARCH/health" || true)
echo "[health] $code"
[ "$code" = "200" ] || { echo "health FAIL"; exit 1; }

echo "OK"
