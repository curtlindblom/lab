# Warehouse Picking

The `warehousepicking` Power Apps code app is a Vite, React and TypeScript app hosted by
Power Apps. It uses Fluent UI for the shell and TALXIS base controls for the record grids,
lookups and forms.

## Run locally

```sh
npm install
npm run dev
```

Vite serves the app at <http://localhost:5173>. Build it with `npm run build`.

## Screens

- **Items** lists warehouse items and opens an item detail form with its category, location,
  quantity on hand and newest-first transactions. Create items or record Inbound and Outbound
  transactions from the list or item detail.
- **Transactions** lists all stock movements newest first and allows a movement to be recorded
  for a selected item.
- **Locations** lists locations with their item counts and allows new locations to be added.

The app reads and writes all three tables through the generated services in `src/generated`.
It validates required fields only; Dataverse decides whether a stock movement is allowed, and
its error message is shown unchanged. Barcode scanning is not included in this checkpoint.
