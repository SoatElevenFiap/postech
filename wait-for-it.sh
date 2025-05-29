#!/bin/bash
# wait-for-it.sh

set -e

sleep 5
echo "Starting wait-for-it.sh" 

shift 1
cmd="$@"

echo "Waiting for Postgres to be available at $POSTGRES_HOST:$POSTGRES_PORT..."

until PGPASSWORD=$POSTGRES_PASSWORD psql -h "$POSTGRES_HOST" -p "$POSTGRES_PORT" -U "$POSTGRES_USER" -d "$POSTGRES_DB" -t -c "SELECT 1" > /dev/null 2>&1; do
  >&2 echo "Postgres is unavailable - sleeping"
  sleep 1
done

>&2 echo "Postgres is up - executing command"
exec $cmd
echo "Command executed: $cmd"