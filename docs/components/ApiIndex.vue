<script setup>
import exportedApi from "../.vitepress/exportedApi";
import NamespaceCard from "./NamespaceCard.vue";
import { ref, computed } from "vue";

const filter = ref("");
const shouldFilter = computed(() => filter.value.length !== 0);
const filteredApi = computed(() => {
  if (!shouldFilter.value) {
    return exportedApi;
  }

  return exportedApi
    .map((ns) => ({
      ...ns,
      members: ns.members
        .map((t) => ({
          ...t,
          members: t.members?.filter(matches) ?? [],
        }))
        .filter((t) => matches(t.name) || t.members.length !== 0),
    }))
    .filter((ns) => ns.members.length !== 0);
});

function matches(memberName) {
  return memberName.toLowerCase().includes(filter.value.toLowerCase());
}
</script>

<template>
  <div class="wrapper">
    <input
      type="text"
      placeholder="Filter"
      class="api-filter"
      v-model="filter"
    />
    <div class="ns-list">
      <NamespaceCard
        v-for="ns in filteredApi"
        :key="ns.name"
        :name="ns.name"
        :members="ns.members"
      />
    </div>
  </div>
</template>

<style scoped>
.ns-list {
  display: flex;
  gap: 12px;
  margin-top: 20px;
  flex-wrap: wrap;
}

.api-filter {
  font-size: 20px;
  border: 1px solid #45454b;
  padding: 4px;
  border-radius: 4px;
  margin-top: 12px;
}
</style>
