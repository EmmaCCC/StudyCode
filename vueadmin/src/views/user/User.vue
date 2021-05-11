<template>
  <div class="user">
    <el-form :inline="true" class="demo-form-inline">
      <el-form-item label="审批人">
        <el-input placeholder="审批人" value="哈哈"></el-input>
      </el-form-item>
      <el-form-item label="活动区域">
        <el-select placeholder="活动区域" value="">
          <el-option label="区域一" value="shanghai"></el-option>
          <el-option label="区域二" value="beijing"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary">查询</el-button>{{param && param.id}}
      </el-form-item>
    </el-form>
    <el-table :data="tableData" border style="width: 100%">
      <el-table-column fixed prop="date" label="日期"></el-table-column>
      <el-table-column prop="name" label="姓名"></el-table-column>
      <el-table-column prop="province" label="省份"></el-table-column>
      <el-table-column prop="city" label="市区"></el-table-column>
      <el-table-column prop="address" label="地址"></el-table-column>
      <el-table-column prop="zip" label="邮编" width="120"></el-table-column>
      <el-table-column label="操作" width="250">
        <template slot-scope="scope">
          <el-button @click="handleClick(scope.row)" type="primary" size="small"
            >查看</el-button
          >
          <el-button type="success" size="small">编辑</el-button>
          <el-button type="danger" size="small">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-pagination
      style="margin-top: 15px"
      @size-change="sizeChange"
      @current-change="pageChange"
      :current-page="query.page"
      :page-sizes="[10, 20, 30]"
      :page-size="query.size"
      layout="total, sizes, prev, pager, next, jumper"
      :total="query.total"
      background
    ></el-pagination>
  </div>
</template>

<script>
import mixin from "@/global/mixin";

export default {
  mixins:[mixin],
  name:'User',
  props: {
    param: { type: Object },
  },
  data() {
    return {
      query: {
        page: 1,
        size: 10,
        total: 0,
      },
      tableData: [
        {
          id:1,
          date: "2016-05-02",
          name: "王小虎1",
          province: "上海",
          city: "普陀区",
          address: "上海市普陀区金沙江路 1518 弄",
          zip: 200333,
        },
        {
          id:2,
          date: "2016-05-04",
          name: "王小虎2",
          province: "上海",
          city: "普陀区",
          address: "上海市普陀区金沙江路 1517 弄",
          zip: 200333,
        },
        {
          id:3,
          date: "2016-05-01",
          name: "王小虎3",
          province: "上海",
          city: "普陀区",
          address: "上海市普陀区金沙江路 1519 弄",
          zip: 200333,
        },
        {
          id:4,
          date: "2016-05-03",
          name: "王小虎4",
          province: "上海",
          city: "普陀区",
          address: "上海市普陀区金沙江路 1516 弄",
          zip: 200333,
        },
      ],
    };
  },
  components: {},
  created() {
   
  },
  methods: {
    pageChange(page) {
      this.query.page = page;
    },
    sizeChange(size) {
      this.query.size = size;
    },
    handleClick(row) {
      let id = row.id;
      var tab = {
        content: "UserDetail",
        name: "UserDetail_" + id,
        title: "人员信息"+id,
        param: { id: id },
      };
      this.$home.addTab(tab);

    },
  },
};
</script>
<style lang="scss" scoped></style>
