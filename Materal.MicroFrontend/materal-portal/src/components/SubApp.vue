<template>
  <div v-html="content"></div>
</template>

<script>
export default {
  name: 'SubApp',
  props: {
    loading: Boolean,
    content: String
  }
}
</script>