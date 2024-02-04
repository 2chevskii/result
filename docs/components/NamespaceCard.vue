<script setup>
defineProps(["name", "members"]);

const memberNameToHeader = (mn) => {
  console.log("MN:", mn);

  return mn
    .replace(/[^a-zA-Z0-9]/g, "-")
    .replace(/-{2,}/g, "-")
    .replace(/(^-|-$)/g, "")
    .toLowerCase();
};
</script>

<template>
  <div class="ns-card">
    <h3 class="ns-name">{{ name }}</h3>
    <ul class="ns-member-list">
      <li class="ns-member" v-for="member in members" :key="member.uriName">
        <a
          :href="`/result/api/${name}/${member.uriName || member.name}`"
          v-html="member.name"
        ></a>
        <ul
          class="type-member-list"
          v-if="member.members && member.members.length"
        >
          <li v-for="tMember in member.members" :key="tMember">
            <a
              :href="`/result/api/${name}/${
                member.uriName || member.name
              }#${memberNameToHeader(tMember)}`"
              >{{ tMember }}</a
            >
          </li>
        </ul>
      </li>
    </ul>
  </div>
</template>

<style scoped>
.ns-card {
  background-color: var(--vp-c-bg-soft);
  padding: 24px;
  border-radius: 8px;
  flex-grow: 1;
}

.ns-name {
  color: var(--vp-c-brand-1);
  font-weight: bold;
  margin-top: 0;
}

.ns-member-list {
  list-style: none;
  padding: 0;
}
</style>
